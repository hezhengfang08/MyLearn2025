﻿using Microsoft.Extensions.Options;
using MySelf.AgileFramework.WebCore.JWTExtend.RSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.AgileFramework.WebCore.JWTExtend
{
    /// <summary>
    /// 简单封装个注入
    /// </summary>
    public interface IJWTService
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        string GetToken(CurrentUserModel userInfo);

        /// <summary>
        /// 获取Token+RefreshToken
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>Token+RefreshToken</returns>
        Tuple<string, string> GetTokenWithRefresh(CurrentUserModel userInfo);

        /// <summary>
        /// 基于refreshToken获取Token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        string GetTokenByRefresh(string refreshToken);

        /// <summary>
        /// 检查是否过期
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool ValidateTokenExpire(string token)
        {
            return JWTTokenDeserialize.ValidateRefreshToken(token);
        }
    }
}
