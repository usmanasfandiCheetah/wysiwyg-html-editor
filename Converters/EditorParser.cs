using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace mages.editor.Converters
{
    public class EditorParser
    {

        public static string ToHtml(string rtfText)
        {
            string result = rtfText;
            if (!string.IsNullOrEmpty(rtfText))
            {
                if (rtfText.Contains("\\rtf"))
                {
                    result = RtfPipe.Rtf.ToHtml(rtfText);

                }
            }

            return result;
        }

        public static string ToRtf(string htmlText)
        {
            //HtmlDocument htmlDoc = new HtmlDocument();
            //htmlDoc.LoadHtml(htmlText);

            //var attributes = htmlDoc.DocumentNode.GetAttributes();
            //foreach (var attribute in attributes)
            //{

            //}
            //if (htmlDoc.DocumentNode.GetClasses() != null)
            //{
            //    var classes = htmlDoc.DocumentNode.GetClasses();
            //}
            //string result = htmlDoc.DocumentNode.InnerText;
            //return result;
            return ConvertHTMLToRTF(htmlText);
        }

        public static string ConvertHTMLToRTF(string html)
        {
            SautinSoft.HtmlToRtf h = new SautinSoft.HtmlToRtf();
            if (!string.IsNullOrEmpty(html)){
                if (h.OpenHtml(html))
                {
                    return h.ToRtf();
                }
            }

            return html;
        }

    }
}
