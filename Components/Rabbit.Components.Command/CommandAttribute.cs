using Rabbit.Kernel.Utility.Extensions;
using System;
using System.Linq;

namespace Rabbit.Components.Command
{
    /// <summary>
    /// �����ǡ�
    /// </summary>
    public sealed class CommandAttribute : Attribute
    {
        /// <summary>
        /// ��ʼ��һ���µ������ǡ�
        /// </summary>
        /// <param name="commandName">�������ơ�</param>
        public CommandAttribute(string commandName)
        {
            CommandName = commandName.NotEmptyOrWhiteSpace("commandName");
        }

        /// <summary>
        /// ��ʼ��һ���µ������ǡ�
        /// </summary>
        /// <param name="commandAction">�������</param>
        /// <param name="commandName">�������ơ�</param>
        public CommandAttribute(CommandAction commandAction, string commandName)
        {
            Action = commandAction;
            CommandName = commandName.NotEmptyOrWhiteSpace("commandName");
        }

        /// <summary>
        /// ��ʼ��һ���µ������ǡ�
        /// </summary>
        /// <param name="commandAction">�������</param>
        /// <param name="commandName">�������ơ�</param>
        /// <param name="description">����˵����</param>
        public CommandAttribute(CommandAction commandAction, string commandName, string description)
            : this(commandName)
        {
            Action = commandAction;
            Description = description.NotEmptyOrWhiteSpace("description");
        }

        /// <summary>
        /// ����˵����
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ����ĵ������ơ�
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public CommandAction Action { get; set; }
    }

    /// <summary>
    /// ���������ǡ�
    /// </summary>
    public sealed class CommandAliasesAttribute : Attribute
    {
        /// <summary>
        /// ��ʼ��һ���µ����������
        /// </summary>
        /// <param name="aliases">���������</param>
        public CommandAliasesAttribute(params string[] aliases)
        {
            Aliases = aliases.NotNull("aliases");
            var count = aliases.Count();
            if (count > aliases.Distinct().Count())
                throw new Exception("���������ظ��ı�����");
        }

        /// <summary>
        /// �������ơ�
        /// </summary>
        public string[] Aliases { get; private set; }
    }
}