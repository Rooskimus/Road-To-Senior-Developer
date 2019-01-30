using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    // This demonstrates the decorator pattern, using an example of a
    // text message that can be emailed, faxed, and/or printed based on
    // need and is also always sent to a database.

    // The Message Class
    public interface IMessage
    {
        string Msg { get; set; }
        void Process();
    }

    public class Message : IMessage
    {
        public string Msg { get; set; }
        public void Process()
        {
            Console.WriteLine(String.Format("Saved '{0}' to database.", Msg));
        }
    }

    // We can now create a base decorator

    public abstract class BaseMessageDecorator : IMessage
    {
        private IMessage innerMessage;
        public BaseMessageDecorator(IMessage decorator)
        {
            innerMessage = decorator;
        }
        public virtual void Process()
        {
            innerMessage.Process();
        }

        public string Msg
        {
            get
            {
                return innerMessage.Msg;
            }
            set
            {
                innerMessage.Msg = value;
            }
        }
    }

    // And some concrete decorators:

    public class EmailDecorator : BaseMessageDecorator
    {
        public EmailDecorator(IMessage decorator)
            : base(decorator) { }
        public override void Process()
        {
            base.Process();
            Console.WriteLine(String.Format($"Sent '{Msg}' as email."));
        }
    }

    public class FaxDecorator : BaseMessageDecorator
    {
        public FaxDecorator(IMessage decorator)
            : base(decorator) { }
        public override void Process()
        {
            base.Process();
            Console.WriteLine(String.Format($"Sent '{Msg}' as fax"));
        }
    }

    public class ExternalSystemDecorator : BaseMessageDecorator
    {
        public ExternalSystemDecorator(IMessage decorator)
            : base(decorator) { }
        public override void Process()
        {
            base.Process();
            Console.WriteLine(String.Format($"Sent '{Msg}' to external system."));
        }
    }


}
