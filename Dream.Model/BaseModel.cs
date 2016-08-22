
namespace Dream.Model
{
    public abstract class BaseModel
    {
        public override string ToString()
        {
            string result = string.Empty;
            var properties = this.GetType().GetProperties();
            foreach (var item in properties)
            {
                result += item.Name + ":" + item.GetValue(this, null) + ",";
            }
            if (properties.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }
    }
}
