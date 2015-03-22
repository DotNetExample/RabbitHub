using Rabbit.Kernel.Logging;
using System;

namespace Rabbit.Components.Logging.NLog
{
    /// <summary>
    /// һ���������־��¼��������
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// ������־��¼����
        /// </summary>
        /// <param name="type">���͡�</param>
        /// <returns>��־��¼��ʵ����</returns>
        ILogger CreateLogger(Type type);
    }
}