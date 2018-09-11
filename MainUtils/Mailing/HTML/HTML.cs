using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Mailing
{
    public class HTML
    {
        StringBuilder builder;

        public HTML()
        {
            builder = new StringBuilder();
        }

        public override string ToString()
        {
            return builder.ToString();
        }

        public void addTable(DataTable dt, string color = "Black")
        {
            if (dt.Rows.Count == 0) return; // enter code here

            builder.Append("<table border='1px' cellpadding='5' cellspacing='0' frame='void' rules='rows'");
            builder.Append("style='border: solid 1px Silver; font-size: x-small;'>");
            builder.Append("<tr align='left' valign='top'>");
            foreach (DataColumn c in dt.Columns)
            {
                builder.Append("<td align='left' valign='top'><b>");
                builder.Append(c.ColumnName);
                builder.Append("</b></td>");
            }
            builder.Append("</tr>");
            foreach (DataRow r in dt.Rows)
            {
                builder.Append($"<tr align='left' valign='top' style ='color:{color};'>");
                foreach (DataColumn c in dt.Columns)
                {
                    builder.Append("<td align='left' valign='top'>");
                    builder.Append(r[c.ColumnName]);
                    builder.Append("</td>");
                }
                builder.Append("</tr>");
            }
            builder.Append("</table>");
            //builder.Append("</body>");
            //builder.Append("</html>");

            return;
        }

        public void AddElement(string content, string tagtype = "p")
        {
            builder.Append($"<{tagtype}/>{content}</{tagtype}>");
        }
        public void addSingleElement(string tagtype)
        {
            builder.Append("<" + tagtype + "/>");

        }


        public void AddBreak()
        {
            builder.Append("<br/><br/>");
        }


        public static string toHTML_Table(DataTable dt, string color = "Black")
        {
            if (dt.Rows.Count == 0) return ""; // enter code here

            StringBuilder builder = new StringBuilder();

            builder.Append("<table border='1px' cellpadding='5' cellspacing='0' frame='void' rules='rows'");
            builder.Append("style='border: solid 1px Silver; font-size: x-small;'>");
            builder.Append("<tr align='left' valign='top'>");


            builder.Append("<td align='left' valign='top'><b>");
            builder.Append("#");
            builder.Append("</b></td>");

            foreach (DataColumn c in dt.Columns)
            {
                builder.Append("<td align='left' valign='top'><b>");
                builder.Append(c.ColumnName);
                builder.Append("</b></td>");
            }
            builder.Append("</tr>");
            int i = 1;
            foreach (DataRow r in dt.Rows)
            {
                builder.Append($"<tr align='left' valign='top' style ='color:{color};'>");

                builder.Append("<td align='left' valign='top'>");
                builder.Append(i);
                builder.Append("</td>");

                foreach (DataColumn c in dt.Columns)
                {
                    builder.Append("<td align='left' valign='top'>");
                    builder.Append(r[c.ColumnName]);
                    builder.Append("</td>");
                }
                builder.Append("</tr>");
                i++;
            }
            builder.Append("</table>");
            //builder.Append("</body>");
            //builder.Append("</html>");

            return builder.ToString();
        }

    }
}
