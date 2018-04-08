﻿// ***********************************************************************
// Assembly         : KsWare.CodeGenerator
// Author           : SchreinerK
// Created          : 03-26-2018
//
// Last Modified By : SchreinerK
// Last Modified On : 03-27-2018
// ***********************************************************************
// <copyright file="MethodGenerator.cs" company="KsWare">
//     Copyright © 2018 KsWare. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace KsWare.CodeGenerator.Generators {

	/// <summary>
	/// Class MethodGenerator.
	/// </summary>
	/// <seealso cref="BaseGenerator" />
	/// <autogeneratedoc />
	public class MethodGenerator : BaseGenerator {

		/// <summary>
		/// Initializes a new instance of the <see cref="MethodGenerator"/> class.
		/// </summary>
		/// <param name="generator">The generator.</param>
		/// <autogeneratedoc />
		public MethodGenerator(Generator generator) : base(generator) { }

		/// <summary>
		/// Generates code for the specified method information.
		/// </summary>
		/// <param name="methodInfo">The method information.</param>
		/// <returns>System.String.</returns>
		public string Generate(MethodInfo methodInfo) => Generate(methodInfo, MethodGeneratorOptions.Create(GeneratorMode));

		/// <summary>
		/// Generates code for the specified method information.
		/// </summary>
		/// <param name="methodInfo">The method information.</param>
		/// <param name="options">The generator options.</param>
		/// <returns>System.String.</returns>
		public string Generate(MethodInfo methodInfo, MethodGeneratorOptions options) {
			var sb = new StringBuilder();

			if (options.Access    ) sb.Append(Generator.Access  (methodInfo.Attributes));
			if (options.Modifiers ) sb.Append(Generator.Modifier(methodInfo.Attributes));
			if (options.ReturnType) sb.Append(Generator.Generate(methodInfo.ReturnType) + " ");
			if (options.Name      ) sb.Append(methodInfo.Name);
			if(methodInfo.IsGenericMethod) sb.Append("<" + Generator.Generate(methodInfo.GetGenericArguments()) + ">");

			//TODO use options
			sb.Append("(");
			sb.Append(Generator.Generate(methodInfo.GetParameters()));
			sb.Append(")");

			if (sb.ToString() == "protected override void Finalize()")
				return $"~{methodInfo.DeclaringType.Name}()"; // Destructor

			return GeneratorMode == GeneratorMode.InheriteDoc 
				? sb.ToString().Replace("<", "{").Replace(">", "}") 
				: sb.ToString();
		}

		/// <summary>
		/// Generates code for the specified constructor information.
		/// </summary>
		/// <param name="constructorInfo">The constructor information.</param>
		/// <returns>System.String.</returns>
		public string Generate(ConstructorInfo constructorInfo) {
			var sb = new StringBuilder();

			if (constructorInfo.IsStatic) sb.Append("static ");
			else sb.Append($"{Generator.Generate(constructorInfo.Attributes)}");


			sb.Append(constructorInfo.Name);
			sb.Append("(");
			sb.Append(Generator.Generate(constructorInfo.GetParameters()));
			sb.Append(")");
			return sb.ToString();
		}

		private static readonly Dictionary<string, string> OperatorNames = new Dictionary<string, string>() {
			{"op_Implicit", "N/A"},
			{"op_Explicit", "N/A"},
			{"op_Addition", "+"},
			{"op_Subtraction", "-"},
			{"op_Multiply", "*"},
			{"op_Division", "/"},
			{"op_Modulus", "%"},
			{"op_ExclusiveOr", "^"},
			{"op_BitwiseAnd", "&"},
			{"op_BitwiseOr", "|"},
			{"op_LogicalAnd", "&&"},
			{"op_LogicalOr", "||"},
			{"op_Assign", "="},
			{"op_LeftShift", "<<"},
			{"op_RightShift", ">>"},
			{"op_SignedRightShift", "N/A"},
			{"op_UnsignedRightShift", "N/A"},
			{"op_Equality", "=="},
			{"op_Inequality", "!="},
			{"op_GreaterThan", ">"},
			{"op_LessThan", "<"},
			{"op_GreaterThanOrEqual", ">="},
			{"op_LessThanOrEqual", "<="},
			{"op_MultiplicationAssignment", "*="},
			{"op_SubtractionAssignment", "-="},
			{"op_ExclusiveOrAssignment", "^="},
			{"op_LeftShiftAssignment", "<<="},
			{"op_ModulusAssignment", "%="},
			{"op_AdditionAssignment", "+="},
			{"op_BitwiseAndAssignment", "&="},
			{"op_BitwiseOrAssignment", "|="},
			{"op_Comma", ","},
			{"op_DivisionAssignment", "/="},
			{"op_Decrement", "--"},
			{"op_Increment", "++"},
			{"op_UnaryNegation", "-"},
			{"op_UnaryPlus", "+"},
			{"op_OnesComplement", "~"},
		};

		internal string Operator(string name) {
			return OperatorNames[name];

			/*
			C# Operator Symbol	Metadata Name		Friendly Name
			N/A			op_Implicit					To<TypeName>/From<TypeName>
			N/A			op_Explicit					To<TypeName>/From<TypeName>
			+			op_Addition					Add (binary)
			-			op_Subtraction				Subtract (binary)
			*			op_Multiply					Multiply (binary)
			/			op_Division					Divide
			%			op_Modulus					Mod or Remainder
			^			op_ExclusiveOr				Xor
			&			op_BitwiseAnd				BitwiseAnd (binary)
			|			op_BitwiseOr				BitwiseOr
			&&			op_LogicalAnd				And
			||			op_LogicalOr				Or
			=			op_Assign					Assign
			<<			op_LeftShift				LeftShift
			>>			op_RightShift				RightShift
			N/A			op_SignedRightShift			SignedRightShift
			N/A			op_UnsignedRightShift		UnsignedRightShift
			==			op_Equality					Equals
			!=			op_Inequality				Equals
			>			op_GreaterThan				CompareTo
			<			op_LessThan					CompareTo
			>=			op_GreaterThanOrEqual		CompareTo
			<=			op_LessThanOrEqual			CompareTo
			*=			op_MultiplicationAssignment	Multiply
			-=			op_SubtractionAssignment	Subtract
			^=			op_ExclusiveOrAssignment	Xor
			<<=			op_LeftShiftAssignment		LeftShift
			%=			op_ModulusAssignment		Mod
			+=			op_AdditionAssignment		Add
			&=			op_BitwiseAndAssignment		BitwiseAnd
			|=			op_BitwiseOrAssignment		BitwiseOr
			,			op_Comma					Comma
			/=			op_DivisionAssignment		Divide
			--			op_Decrement				Decrement
			++			op_Increment				Increment
			- (unary)	op_UnaryNegation			Negate
			+ (unary)	op_UnaryPlus				Plus
			~			op_OnesComplement			OnesComplement
			*/
		}
	}
}
