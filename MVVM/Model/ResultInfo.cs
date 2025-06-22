using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poll_ver2.MVVM.Model
{
    public class ResultInfo
    {
        public Result ResultGroup1 {  get; set; }
        public Result ResultGroup2 { get; set; }
        public Result ResultGroup3 { get; set; }
    }

    public class Result
    {
        public string Paragraph1Heading {  get; set; }
        public string Paragraph2Heading {  get; set; }
        public string Paragraph3Heading { get; set; }
        public string Paragraph1Text {  get; set; }
        public string Paragraph2Text {  get; set; }
        public string Paragraph3Text {  get; set; }
        public string Heading {  get; set; }
        public string Text1 {  get; set; }
        public string Text1_1 { get; set; }
    }
}
