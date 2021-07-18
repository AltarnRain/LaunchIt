using System.Collections.Generic;

namespace Infrastructure
{
    public static class ExtensionClass
    {
        public static Dictionary<int, int> GetMaxArrayContent(this IEnumerable<IEnumerable<string>> elements)
        {
            var returnValue = new Dictionary<int, int>();

            foreach (var cachedLog in elements)
            {
                var i = 0;
                foreach (var element in  cachedLog)
                {
                    if (!returnValue.ContainsKey(i))
                    {
                        returnValue.Add(i, element.Length);
                    }
                    else
                    {
                        if (returnValue[i] < element.Length)
                        {
                            returnValue[i] = element.Length;
                        }
                    }

                    i++;
                }
            }

            return returnValue;
        }
    }
}
