using System;

namespace Ex03.GarageLogic
{
    public class Parameter
    {
        private readonly Type r_ParameterType;
        private readonly string r_ParameterName;
        private object m_Content;

        public Parameter(Type i_paramType, string i_paramName, object i_paramContent)
        {
            r_ParameterType = i_paramType;
            r_ParameterName = i_paramName;
            m_Content = i_paramContent;
        }

        public Type ParameterType
        {
            get
            {
                return r_ParameterType;
            }
        }

        public object ParameterContent
        {
            get
            {
                return m_Content;
            }

            set
            {
                m_Content = value; 
            }
        }

        public string ParameterName
        {
            get
            {
                return r_ParameterName;
            }
        }

        public T ConvertEnum<T>()
        {
            T enumVal = (T)Enum.Parse(typeof(T), m_Content.ToString());
            return enumVal;
        }
    }
}
