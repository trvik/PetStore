namespace Modals
{
    public class Tag
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string GetPropertyValue(string propName) => this.GetType().GetProperty(propName).GetValue(this, null).ToString();

        public void SetPropertyValue(string propName, string propValue) => this.GetType().GetProperty(propName).SetValue(this, propValue);
    }
}
