using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Xml;

namespace SuitCEyes
{
    public class Document
    {
        public List<Pattern> _patterns = new List<Pattern>();
        public List<PatternTemplate> _templatePatterns = new List<PatternTemplate>();

        public bool IsModified { get; set; }
        public string Filename { get; set; }

        public Document()
        {
            IsModified = false;
        }

        public bool IsValidName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            foreach(Pattern p in _patterns)
            {
                if (name == p.Name)
                    return false;
            }

            return true;
        }

        public PatternTemplate FindTemplate(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            foreach (PatternTemplate template in _templatePatterns)
            {
                if (name == template.Name)
                    return template;
            }

            return null;
        }

        public void AddNewPattern(Pattern pattern)
        {
            IsModified = true;
            _patterns.Add(pattern);
        }

        public void Delete(Pattern pattern)
        {
            IsModified = true;
            _patterns.Remove(pattern);
        }

        #region Storages
        public void Load(string filename)
        {
            Filename = filename;

            XmlDocument doc = new XmlDocument();          
            doc.Load(Filename);

            // Load all templates
            XmlNode templates = doc.SelectSingleNode("/database/templates");
            if (templates == null)
                throw new System.Exception("No templates not found in file. Correct format?");

            foreach (XmlNode template in templates)
            {   
                string name = template.Attributes["name"].Value;
                string height = template.Attributes["height"].Value;
                string width = template.Attributes["width"].Value;

                PatternTemplate a = new PatternTemplate(name, XmlConvert.ToInt32(width), XmlConvert.ToInt32(height));

                // Check if color is defined
                XmlNode color_node = template.SelectSingleNode("color");
                if(color_node != null)
                {
                    string color_text = color_node.InnerText;                    
                    string[] colors = color_text.Split(new char[] { ',' });
                    foreach (string color in colors)
                    {
                        string[] c = color.Split(new char[] { ';' });
                        if (c == null || c.Length != 2)
                            throw new Exception("Unable to parse color information");

                        a.SetCellBackgroundColor(Convert.ToInt32(c[0]), System.Drawing.Color.FromArgb(Convert.ToInt32(c[1], 16)));
                    }
                }

                // Check if remap is defined
                XmlNode remap_node = template.SelectSingleNode("remap");
                if (remap_node != null)
                {
                    string remap_text = remap_node.InnerText;
                    string[] remaps = remap_text.Split(new char[] { ',' });
                    foreach (string remap in remaps)
                    {
                        string[] c = remap.Split(new char[] { ';' });
                        if (c == null || c.Length != 2)
                            throw new Exception("Unable to parse remap information");

                        a.SetRemapChannel(Convert.ToInt32(c[0]), Convert.ToInt32(c[1]));
                    }
                }

                _templatePatterns.Add(a);
            }

            // Load all haptograms
            XmlNode haptograms = doc.SelectSingleNode("/database/haptograms");
            foreach (XmlNode haptogram in haptograms)
            {
                string name = haptogram.Attributes["name"].Value;
                string template_name = haptogram.Attributes["template"].Value;

                // Make sure template exists
                PatternTemplate template = FindTemplate(template_name);
                if(template == null)
                    throw new System.Exception("Template " + template_name + " not defined.");

                Pattern p = new Pattern(name, template);

                foreach (XmlNode frame in haptogram.SelectNodes("frames/frame")) {
                    string duration_text = frame.Attributes["duration"].Value;
                    int duration = Convert.ToInt32(duration_text);

                    // Extrach channel info
                    string data = frame.InnerText;
                    string[] channels = data.Split(new char[] { ',' } );
                    List<int> values = new List<int>();               
                    foreach(string t in channels)
                    {
                        values.Add(Convert.ToInt32(t.Trim()));
                    }

                    p.AddNewFrame(duration, values.ToArray());
                }

                _patterns.Add(p);
            }

            IsModified = false;
        }

        public void Save()
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = false,
                NewLineOnAttributes = false
            };

            var textWriter = XmlWriter.Create(Filename, settings);
            // Opens the document  
            textWriter.WriteStartDocument();
            textWriter.WriteStartElement("database");
            textWriter.WriteAttributeString("version", FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);

            textWriter.WriteStartElement("templates");
                foreach(PatternTemplate t in _templatePatterns)
            {
                textWriter.WriteStartElement("template");
                textWriter.WriteAttributeString("name", t.Name);
                textWriter.WriteAttributeString("width", t.Width.ToString());
                textWriter.WriteAttributeString("height", t.Height.ToString());

                if (t.CellBackgroundColorDict != null)
                {
                    textWriter.WriteStartElement("color");
                    StringBuilder colors = new StringBuilder();
                    bool first = true;
                    foreach (KeyValuePair<int, System.Drawing.Color> kp in t.CellBackgroundColorDict)
                    {
                        if (!first)
                            colors.Append(",");
                        else
                            first = false;

                        colors.AppendFormat("{0};{1:X}", kp.Key, kp.Value.ToArgb());
                    }
                    textWriter.WriteString(colors.ToString());
                    textWriter.WriteEndElement();
                }

                if (t.RemapChannelsDict != null)
                {
                    textWriter.WriteStartElement("remap");
                    StringBuilder remaps = new StringBuilder();
                    bool first = true;
                    foreach (KeyValuePair<int, int> kp in t.RemapChannelsDict)
                    {
                        if (!first)
                            remaps.Append(",");
                        else
                            first = false;

                        remaps.AppendFormat("{0};{1}", kp.Key, kp.Value);
                    }
                    textWriter.WriteString(remaps.ToString());
                    textWriter.WriteEndElement();
                }

                textWriter.WriteEndElement();
            }
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("haptograms");
            foreach (Pattern p in _patterns)
            {
                textWriter.WriteStartElement("haptogram");
                textWriter.WriteAttributeString("name", p.Name);                
                textWriter.WriteAttributeString("template", p.Template.Name);

                textWriter.WriteStartElement("frames");                
                foreach (Frame frame in p.Frames)
                {
                    textWriter.WriteStartElement("frame");
                    textWriter.WriteAttributeString("duration", frame.Duration.ToString());

                    StringBuilder channels = new StringBuilder();
                    bool first = true;
                    foreach(int c in frame.Channels)
                    {
                        if (!first)
                            channels.Append(",");
                        else
                            first = false;
                        channels.Append(c);
                        
                    }
                    textWriter.WriteString(channels.ToString());

                    textWriter.WriteEndElement();
                }
                textWriter.WriteEndElement();

                textWriter.WriteEndElement();
            }
            
            textWriter.WriteEndElement();
            textWriter.WriteEndDocument();            
            textWriter.Close();

            IsModified = false;
        }

        #endregion
    }
}
