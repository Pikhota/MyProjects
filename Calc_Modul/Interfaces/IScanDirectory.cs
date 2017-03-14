namespace Calc_Service.Interfaces
{
    internal interface IScanDirectory
    {
        string PathDir { get; set; }
        string PathResult { get; set;}
        void Scan(string _pathDirectory);
        string DataProcessing(string _str);
    }
}
