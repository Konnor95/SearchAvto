namespace SearchAvto.Models.LogicModels
{
    public interface ISender
    {
        void Send(string topic, string text, string userMail);
    }
}
