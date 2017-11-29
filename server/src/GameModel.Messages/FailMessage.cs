using System.Runtime.Serialization;

namespace GameModel.Messages
{
    /// <summary>
    /// Emitted when the master arbitrator is ready to begin the game
    /// </summary>
    [DataContract]
    public class FailMessage : ModelMessage
    {

    }
}