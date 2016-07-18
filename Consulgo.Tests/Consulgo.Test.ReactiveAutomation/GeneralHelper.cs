using System;

namespace Consulgo.Test.ReactiveAutomation
{
    internal static class GeneralHelper
    {
        public static void ThrowIfEmpty(this Array array, string paramName, string message = null)
        {
            if(array.Length == 0)
            {
                ArgumentOutOfRangeException arex = null;

                if (string.IsNullOrEmpty(paramName))
                {
                    arex = new ArgumentOutOfRangeException();
                }
                else
                {
                    if(string.IsNullOrEmpty(message))
                    {
                        arex = new ArgumentOutOfRangeException(paramName);
                    }
                    else
                    {
                        arex = new ArgumentOutOfRangeException(paramName, message);
                    }
                }

                throw arex;
            }
        }

        public static void ThrowIfNullArg(this object @object, string paramName, string message = null)
        {
            if (@object == null)
            {
                ArgumentNullException anex = null;

                if (string.IsNullOrEmpty(paramName))
                {
                    anex = new ArgumentNullException();
                }
                else
                {
                    if (string.IsNullOrEmpty(message))
                    {
                        anex = new ArgumentNullException(paramName);
                    }
                    else
                    {
                        anex = new ArgumentNullException(paramName, message);
                    }
                }

                throw anex;
            }
        }

        public static void ThrowIfNull(this object @object, string message = null)
        {
            if (@object == null)
            {
                NullReferenceException nrex = null;

                if (string.IsNullOrEmpty(message))
                {
                    nrex = new NullReferenceException();
                }
                else
                {
                    nrex = new NullReferenceException(message);
                }

                throw nrex;
            }
        }

        public static void ThrowIfNullOrEmpty(this string @object, string message = null)
        {
            if (string.IsNullOrEmpty(@object))
            {
                NullReferenceException nrex = null;

                if (string.IsNullOrEmpty(message))
                {
                    nrex = new NullReferenceException();
                }
                else
                {
                    nrex = new NullReferenceException(message);
                }

                throw nrex;
            }
        }

        public static void ThrowIfNullOrEmptyArg(this string @object, string paramName, string message = null)
        {
            if (string.IsNullOrEmpty(@object))
            {
                ArgumentNullException anex = null;

                if (string.IsNullOrEmpty(paramName))
                {
                    anex = new ArgumentNullException();
                }
                else
                {
                    if (string.IsNullOrEmpty(message))
                    {
                        anex = new ArgumentNullException(paramName);
                    }
                    else
                    {
                        anex = new ArgumentNullException(paramName, message);
                    }
                }

                throw anex;
            }
        }
    }
}
