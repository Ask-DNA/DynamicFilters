using System.Reflection;

namespace DynamicFilters
{
    internal class PropertyOrFieldInfo
    {
        public MemberInfo Wrappee { get; init; }

        public Type PropertyOrFieldType { get; init; }

        public PropertyOrFieldInfo(PropertyInfo wrappee)
        {
            Wrappee = wrappee;
            PropertyOrFieldType = wrappee.PropertyType;
        }

        public PropertyOrFieldInfo(FieldInfo wrappee)
        {
            Wrappee = wrappee;
            PropertyOrFieldType = wrappee.FieldType;
        }

        public static PropertyOrFieldInfo? GetOrDefault(Type instanceType, string memberName, bool mustBeReadable = false)
        {
            PropertyInfo? p = instanceType.GetProperty(memberName);
            if (p is not null)
            {
                if ((!mustBeReadable) || (p.GetGetMethod(false) is not null))
                    return new(p);
            }

            FieldInfo? f = instanceType.GetField(memberName);
            if (f is not null)
            {
                if ((!mustBeReadable) || f.IsPublic)
                    return new(f);
            }
            return null;
        }

        public object? GetValue(object? source)
        {
            if (Wrappee is PropertyInfo p)
                return p.GetValue(source);
            if (Wrappee is FieldInfo f)
                return f.GetValue(source);
            throw new InvalidOperationException("Object state is damaged");
        }

        public static implicit operator PropertyOrFieldInfo(PropertyInfo wrappee)
        {
            return new(wrappee);
        }

        public static implicit operator PropertyOrFieldInfo(FieldInfo wrappee)
        {
            return new(wrappee);
        }

        public static implicit operator MemberInfo(PropertyOrFieldInfo propertyOrFieldInfo)
        {
            return propertyOrFieldInfo.Wrappee;
        }
    }
}
