﻿#region Using directives

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Diagnostics.Contracts;

#endregion

[assembly: CLSCompliant(true)]
[assembly: ContractVerification ( false )]

[assembly: InternalsVisibleTo("NUnit.BitSetArray")]
[assembly: InternalsVisibleTo("NUnit.Enumerables")]
[assembly: InternalsVisibleTo("NUnit.Diagnostics")]
[assembly: InternalsVisibleTo("NUnit.Extensions")]
[assembly: InternalsVisibleTo("NUnit.ICodeSet")]

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("DBCL")]
[assembly: AssemblyDescription("Dragon Base Class Library")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("DBCL")]
[assembly: AssemblyCopyright("DDur@GitHub Copyright 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// This sets the default COM visibility of types in the assembly to invisible.
// If you need to expose a type to COM, use [ComVisible(true)] on that type.
[assembly: ComVisible(false)]

// The assembly version has following format :
//
// Major.Minor.Build.Revision
//
// You can specify all the values or you can use the default the Revision and 
// Build Numbers by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.*")]
