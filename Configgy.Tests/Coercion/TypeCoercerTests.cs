﻿using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Configgy.Coercion;

namespace Configgy.Tests.Coercion
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TypeCoercerTests
    {
        [TestMethod]
        public void Coerce_Returns_Correct_Type_Instance_For_Known_Types()
        {
            const string value = "System.Int32";
            var expected = typeof(int);

            var coercer = new TypeCoercerAttribute();

            Type result;
            var coerced = coercer.Coerce(value, null, null, new CoercionContext( coercer ), out result);

            Assert.AreSame(expected, result);
            Assert.IsTrue(coerced);
        }

        [TestMethod]
        public void Coerce_Returns_Null_For_Unknown_Types()
        {
            const string value = "System.Int365";

            var coercer = new TypeCoercerAttribute();

            Type result;
            var coerced = coercer.Coerce(value, null, null, new CoercionContext( coercer ), out result);

            Assert.IsNull(result);
            Assert.IsFalse(coerced);
        }

        [TestMethod]
        public void Coerce_Returns_Null_For_Invlaid_Type_Name()
        {
            const string value = "000 Banana Kerfuffle Ogre !@#$%^&*()_+";

            var coercer = new TypeCoercerAttribute();

            Type result;
            var coerced = coercer.Coerce(value, null, null, new CoercionContext( coercer ), out result);

            Assert.IsNull(result);
            Assert.IsFalse(coerced);
        }

        [TestMethod]
        public void Coerce_Returns_Null_For_Types_That_Arent_Type()
        {
            const int expected = default(int);
            const string value = "System.Int32";

            var coercer = new TypeCoercerAttribute();

            int result;
            var coerced = coercer.Coerce(value, null, null, new CoercionContext( coercer ), out result);

            Assert.AreEqual(expected, result);
            Assert.IsFalse(coerced);
        }
    }
}