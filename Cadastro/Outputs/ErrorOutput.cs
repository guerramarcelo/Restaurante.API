namespace Outputs
{
    public class ErrorOutput
    {

        public ErrorOutput()
        {
            Validations = new List<string>();
        }

        public List<string> Validations { get; set; }

        public bool HasErrors => Validations.Any();
        public void AddError(string error)
        {
            Validations.Add(error);
        }
    }
}
