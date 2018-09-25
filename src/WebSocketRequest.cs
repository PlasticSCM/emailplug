using System;
using log4net;

namespace EmailPlug
{
    class WebSocketRequest
    {
        internal WebSocketRequest(Config config)
        {
            mConfig = config;
        }

        internal string ProcessMessage(string rawMessage)
        {
            string requestId = Messages.GetRequestId(rawMessage);
            try
            {
                NotificationMessage message = Messages.ReadNotificationMessage(rawMessage);
                EmailNotification.Notify(message.Message, message.Recipients, mConfig);
                return Messages.BuildSuccessfulResponse(requestId);
            }
            catch (Exception ex)
            {
                mLog.ErrorFormat("Error processing message:\n{0}. Error: {1}",
                    rawMessage, ex.Message);
                mLog.DebugFormat("StackTrace: {0}", ex.StackTrace);
                return Messages.BuildErrorResponse(requestId, ex.Message);
            }
        }

        readonly Config mConfig;
        static readonly ILog mLog = LogManager.GetLogger("emailplug");
    }
}
