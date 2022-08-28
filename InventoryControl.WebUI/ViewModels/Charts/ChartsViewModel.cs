namespace InventoryControl.WebUI.ViewModels.Charts
{
    public class Graph3SeriesViewModel
    {
        public ModeloGraphCharts Serie1 { get; set; } = new ModeloGraphCharts();
        public ModeloGraphCharts Serie2 { get; set; } = new ModeloGraphCharts();
        public ModeloGraphCharts Serie3 { get; set; } = new ModeloGraphCharts();
    }

    public class ModeloGraphCharts
    {
        public IList<string> Labels = new List<string>();
        public IList<Int64> Data = new List<Int64>();
        public List<DataDados> Dados = new List<DataDados>();
        public string SerieName { get; set; }
    }

    public class DataDados
    {
        public double x { get; set; }
        public double y { get; set; }
        public string Divisor { get; set; }
        public string Dividendo { get; set; }
        public string Nome1 { get; set; }
        public string Nome2 { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public string BackgroundColor { get; set; }
    }
}
