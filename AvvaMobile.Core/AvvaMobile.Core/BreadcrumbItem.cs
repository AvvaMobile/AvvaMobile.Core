namespace AvvaMobile.Core
{
    public class BreadcrumbItem
    {
        public BreadcrumbItem(string name, string url)
        {
            Name = name;
            Url = url;
        }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}