using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace mages.editor.Converters
{
    public class RichTextBoxDependency : DependencyObject
    {
        private static HashSet<Thread> _recursionProtection = new HashSet<Thread>();

        public static string GetDocumentXaml(DependencyObject obj)
        {
            return (string)obj.GetValue(DocumentXamlProperty);
        }

        public static void SetDocumentXaml(DependencyObject obj, string value)
        {
            _recursionProtection.Add(Thread.CurrentThread);
            obj.SetValue(DocumentXamlProperty, value);
            _recursionProtection.Remove(Thread.CurrentThread);
        }

        public static readonly DependencyProperty DocumentXamlProperty = DependencyProperty.RegisterAttached(
            "DocumentXaml",
            typeof(string),
            typeof(RichTextBoxDependency),
            new FrameworkPropertyMetadata(
                "",
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (obj, e) =>
                {
                    if (_recursionProtection.Contains(Thread.CurrentThread))
                        return;

                    var richTextBox = (RichTextBox)obj;
                    
                    try
                    {
                        var xaml = GetDocumentXaml(obj);
                        var input = EditorParser.ToRtf(xaml);
                        var stream = new MemoryStream(Encoding.UTF8.GetBytes(input));
                        var doc = new FlowDocument();
                        var range = new TextRange(doc.ContentStart, doc.ContentEnd);
                        range.Load(stream, DataFormats.Rtf);
                        //var doc = (FlowDocument)XamlReader.Load(stream);

                        richTextBox.Document = (FlowDocument)doc;
                    }
                    catch (Exception)
                    {
                        richTextBox.Document = new FlowDocument();
                    }

                    richTextBox.TextChanged += (obj2, e2) =>
                    {
                        RichTextBox richTextBox2 = obj2 as RichTextBox;
                        if (richTextBox2 != null)
                        {
                            var output = XamlWriter.Save(richTextBox2.Document);

                            MemoryStream buffer = new MemoryStream();
                            var doc = richTextBox2.Document;
                            //doc = (FlowDocument)XamlReader.Parse(output);
                            var range = new TextRange(doc.ContentStart, doc.ContentEnd);
                            //SetDocumentXaml(richTextBox, range.Text);

                            range.Save(buffer, DataFormats.Rtf);
                            var html = RtfPipe.Rtf.ToHtml(Encoding.UTF8.GetString(buffer.ToArray()));
                            SetDocumentXaml(richTextBox2, html);
                        }
                    };
                }
            )
        );

        //    public static readonly DependencyProperty DocumentXamlProperty =
        //DependencyProperty.RegisterAttached(
        //  "DocumentXaml",
        //  typeof(string),
        //  typeof(RichTextBoxDependency),
        //  new FrameworkPropertyMetadata
        //  {
        //      BindsTwoWayByDefault = true,
        //      PropertyChangedCallback = (obj, e) =>
        //      {
        //          var richTextBox = (RichTextBox)obj;

        //          // Parse the XAML to a document (or use XamlReader.Parse())
        //          var xaml = GetDocumentXaml(obj);
        //          var input = EditorParser.ToRtf(xaml);
        //          //var doc = richTextBox.Document;

        //          var doc = new FlowDocument();
        //          var range = new TextRange(doc.ContentStart, doc.ContentEnd);            

        //          range.Load(new MemoryStream(Encoding.UTF8.GetBytes(input)), DataFormats.Rtf);

        //          // Set the document
        //          richTextBox.Document = doc;

        //          // When the document changes update the source

        //          richTextBox.TextChanged += (obj2, e2) =>
        //          {
        //              RichTextBox rtb = (RichTextBox)obj2;
        //              if (rtb.Document == doc)
        //              {
        //                  //MemoryStream buffer = new MemoryStream();
        //                  //range.Save(buffer, DataFormats.Rtf);
        //                  //var output = XamlWriter.Save(rtb.Document);
        //                  //SetDocumentXaml(rtb, Encoding.UTF8.GetString(buffer.ToArray()));
        //                  //SetDocumentXaml(rtb, output);
        //                  GetDocumentXaml(rtb);
        //              }
        //          };
        //          //range.Changed
        //      }
        //  });


    }
}
