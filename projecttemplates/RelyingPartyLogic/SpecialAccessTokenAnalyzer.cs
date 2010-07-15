﻿//-----------------------------------------------------------------------
// <copyright file="SpecialAccessTokenAnalyzer.cs" company="Andrew Arnott">
//     Copyright (c) Andrew Arnott. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace RelyingPartyLogic {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Cryptography;
	using System.Text;

	using DotNetOpenAuth.OAuth2;

	internal class SpecialAccessTokenAnalyzer : StandardAccessTokenAnalyzer {
		/// <summary>
		/// Initializes a new instance of the <see cref="SpecialAccessTokenAnalyzer"/> class.
		/// </summary>
		/// <param name="authorizationServerPublicSigningKey">The authorization server public signing key.</param>
		/// <param name="resourceServerPrivateEncryptionKey">The resource server private encryption key.</param>
		internal SpecialAccessTokenAnalyzer(RSAParameters authorizationServerPublicSigningKey, RSAParameters resourceServerPrivateEncryptionKey)
			: base(authorizationServerPublicSigningKey, resourceServerPrivateEncryptionKey) {
		}

		public override bool TryValidateAccessToken(DotNetOpenAuth.Messaging.IDirectedProtocolMessage message, string accessToken, out string user, out string scope) {
			bool result = base.TryValidateAccessToken(message, accessToken, out user, out scope);
			if (result) {
				// Ensure that clients coming in this way always belong to the oauth_client role.
				scope += " " + "oauth_client";
			}

			return result;
		}
	}
}
