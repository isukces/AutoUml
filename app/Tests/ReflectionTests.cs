using System;
using System.Linq;
using AutoUml;
using Xunit;

namespace Tests;

public class ReflectionTests
{
    private static string ScanP(Type t, ReflectionFlags flags)
    {
        var x = ClassMemberScannerVisitor
            .ScanProperties(t, flags)
            .Select(a => a.Name)
            .OrderBy(a => a);
        return string.Join("\r\n", x);
    }

    [Fact]
    public void T01_ShouldScanProperties()
    {
        var l = ScanP(typeof(T1), ReflectionFlags.Property);
        var expected = @"InstanceAutoPropertyA
InstanceAutoPropertyAA
InstanceAutoPropertyAB
InstanceAutoPropertyAC
InstanceAutoPropertyB
InstanceAutoPropertyBA
InstanceAutoPropertyBB
InstanceAutoPropertyBC
InstanceAutoPropertyCA
InstanceAutoPropertyCB
InstanceAutoPropertyCC";
        Assert.Equal(expected, l);
    }

    private class T1
    {
        public int InstanceAutoPropertyAA { get; set; }
        public int InstanceAutoPropertyA  { get; }
        public int InstanceAutoPropertyAB { get;           protected set; }
        public int InstanceAutoPropertyAC { get;           private set; }
        public int InstanceAutoPropertyBA { protected get; set; }
        public int InstanceAutoPropertyCA { private get;   set; }

            
        protected int InstanceAutoPropertyBB { get; set; }
        protected int InstanceAutoPropertyB  { get; }
        protected int InstanceAutoPropertyBC { get;         private set; }
        protected int InstanceAutoPropertyCB { private get; set; }

             

        private int InstanceAutoPropertyCC { get; set; }
    }
}