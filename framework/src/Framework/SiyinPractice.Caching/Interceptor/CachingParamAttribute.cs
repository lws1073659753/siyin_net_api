﻿namespace SiyinPractice.Caching.Interceptor
{
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    public class CachingParamAttribute : Attribute
    {
        public CachingParamAttribute()
        {
        }
    }
}