using GameModel.Messages;
using NUnit.Framework;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace GameModel.Client.Tests
{
    [TestFixture]
    public class MessagesSerializableTests
    {
        /// <summary>
        /// Checks to make sure a message type is Serializable
        /// </summary>
        /// <param name="msg">Message type to check</param>
        [Test, TestCaseSource(nameof(MessageTypes))]
        public void MessagesAreSerializable(Type msgType)
        {
            var msg = CreatePhoneyObject(msgType);
            var stream = AttemptToSerialize(msg);
            stream.Position = 0;
            Debug.WriteLine(new StreamReader(stream).ReadToEnd());
        }
        public static IEnumerable MessageTypes
        {
            get
            {
                var assembly = typeof(ModelMessage).Assembly;
                foreach(var type in assembly.ExportedTypes)
                {
                    if (type.IsAbstract)
                    {
                        Debug.WriteLine(String.Format("Skipping abstract class {0}", type.Name));
                    }
                    else
                    {
                        yield return new TestCaseData(type)
                                        .SetName(String.Format("Is Serializable: {0}", type.ToString()))
                                        .SetCategory(nameof(MessagesSerializableTests));
                    }
                }
                yield break;
            }
        }

        private static Stream AttemptToSerialize(object source)
        {
            var formatter = new DataContractSerializer(source.GetType());
            var stream = new MemoryStream();
            formatter.WriteObject(stream, source);
            return stream;
        }

        /// <summary>
        /// Reflection magic
        /// </summary>
        /// <param name="type">Type to magic into existance</param>
        /// <returns>A fresh object</returns>
        private static object CreatePhoneyObject(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            else
            {
                if (type.IsAbstract)
                {
                    //This is a little brittle if there is no concrete impl to back an abstract class
                    type = type.Assembly.ExportedTypes.First(t => t.IsSubclassOf(type));
                }
                var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                var constructor = constructors
                    .FirstOrDefault();
                if (constructor == null)
                {
                    return Activator.CreateInstance(type);
                }
                var parameters = constructor.GetParameters()
                    .Select(p => CreatePhoneyObject(p.ParameterType))
                    .ToArray();
                return constructor.Invoke(parameters);
            }
        }
    }
}
