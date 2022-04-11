using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;

namespace mages.editor.Converters
{
    public class EditorContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // HTML to RTF Format conversion
            if (value != null)
            {
                var content = (string)value;
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(content);
                string result = htmlDoc.DocumentNode.InnerText;
                //result = result.Replace("&copy;", "©");

                return result;
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // RTF Format to HTML conversion
            if (value != null)
            {
                var content = (string)value;
                //var doc = new FlowDocument();
                //var range = new TextRange(doc.ContentStart, doc.ContentEnd);

                //range.Load(new MemoryStream(Encoding.UTF8.GetBytes(content)), DataFormats.Rtf);
                //string result = range.Text;

                //return content;
            }

            return "";
        }
    }
}
