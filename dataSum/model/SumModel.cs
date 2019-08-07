using SqlSugar;

namespace dataSum.model
{
    [SugarTable("data_sum")]
    public class SumModel
    {
        public int id { get; set; }

        public string key { get; set; }

        public string sum { get; set; }

        public string add_date { get; set; }

        public string update_date { get; set; }

        public string path { get; set; }
    }
}
