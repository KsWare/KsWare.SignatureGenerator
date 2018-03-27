﻿// ***********************************************************************
// Assembly         : KsWare.SignatureGenerator
// Author           : SchreinerK
// Created          : 03-27-2018
//
// Last Modified By : SchreinerK
// Last Modified On : 03-27-2018
// ***********************************************************************
// <copyright file="MemberSignatureOptions.cs" company="KsWare">
//     Copyright © 2018 KsWare. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace KsWare.SignatureGenerator.Generators {

	/// <summary>
	/// Class MemberSignatureOptions.
	/// </summary>
	/// <autogeneratedoc />
	public class MemberSignatureOptions {

		/// <summary>
		/// Gets or sets a value indicating whether access is included.
		/// </summary>
		/// <value><c>true</c> if access is included; otherwise, <c>false</c>.</value>
		public bool Access { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this modifiers are included.
		/// </summary>
		/// <value><c>true</c> if modifiers are included; otherwise, <c>false</c>.</value>
		public bool Modifiers { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether return type is included.
		/// </summary>
		/// <value><c>true</c> if return type is included; otherwise, <c>false</c>.</value>
		public bool ReturnType { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether name is included.
		/// </summary>
		/// <value><c>true</c> if name is included; otherwise, <c>false</c>.</value>
		public bool Name { get; set; }

		/// <summary>
		/// Creates the options for the specified mode.
		/// </summary>
		/// <param name="mode">The mode.</param>
		/// <returns>MemberSignatureOptions.</returns>
		/// <autogeneratedoc />
		public static MemberSignatureOptions Create(SignatureMode mode) {
			switch (mode) {
				case SignatureMode.CompareIgnoreReturnType: return new MemberSignatureOptions {Access = true, Modifiers = true, ReturnType =false, Name  = true};
				default:                                    return new MemberSignatureOptions {Access = true, Modifiers = true, ReturnType = false, Name = true};
			}
		}
	}

}