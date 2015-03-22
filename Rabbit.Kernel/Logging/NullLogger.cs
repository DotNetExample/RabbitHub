using System;

namespace Rabbit.Kernel.Logging
{
    /// <summary>
    /// һ���յ���־��¼����
    /// </summary>
    public class NullLogger : ILogger
    {
        #region Field

        private static readonly ILogger Logger = new NullLogger();

        #endregion Field

        #region Property

        /// <summary>
        /// ��¼��ʵ����
        /// </summary>
        public static ILogger Instance
        {
            get { return Logger; }
        }

        #endregion Property

        #region Implementation of ILogger

        /// <summary>
        /// �ж���־��¼���Ƿ�����
        /// </summary>
        /// <param name="level">��־�ȼ���</param>
        /// <returns>�����������true�����򷵻�false��</returns>
        public bool IsEnabled(LogLevel level)
        {
            return false;
        }

        /// <summary>
        /// ��¼��־��
        /// </summary>
        /// <param name="level">��־�ȼ���</param>
        /// <param name="exception">�쳣��</param>
        /// <param name="format">��ʽ��</param>
        /// <param name="args">������</param>
        public void Log(LogLevel level, Exception exception, string format, params object[] args)
        {
        }

        #endregion Implementation of ILogger
    }
}