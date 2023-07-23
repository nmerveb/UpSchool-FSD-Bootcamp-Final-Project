/* eslint-disable @typescript-eslint/no-unsafe-argument */
/* eslint-disable @typescript-eslint/no-unsafe-member-access */
/* eslint-disable @typescript-eslint/no-unsafe-assignment */
import axios from 'axios';
import { LocalJwt, LocalUser } from '../types/AuthTypes';
import { useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
import { getClaimsFromJwt } from './jwtHelper';

const api = axios.create({
  baseURL: 'https://localhost:7287/api',
});

// const refreshAccessToken = async () => {
//   const { setToken } = useContext(AuthContext);

//   try {
//     const refreshToken = '';

//     const response = await axios.post(
//       'https://localhost:7287/api/Authentication/RefreshToken',
//       { refreshToken }
//     );

//     const localJwt: LocalJwt = response.data;

//     const decodedJwt = getClaimsFromJwt(localJwt.accessToken);

//     const localUser: LocalUser = {
//       id: decodedJwt.uid,
//       email: decodedJwt.email,
//       firstName: decodedJwt.given_name,
//       lastName: decodedJwt.family_name,
//       expires: localJwt.expires,
//       accessToken: localJwt.accessToken,
//     };

//     setToken(localUser);

//     return localUser.accessToken;
//   } catch {
//     throw 'refresh error.';
//   }
// };

api.interceptors.request.use((config) => {
  const { token } = useContext(AuthContext);

  if (token) {
    config.headers['Authorization'] = `Bearer ${token.accessToken}`;
  }

  return config;
});

// api.interceptors.response.use(
//   (response) => response,
//   async (error) => {
//     const originalRequest = error.config;

//     // Check if the error response status is 401 Unauthorized and if it's not a retry attempt
//     if (error.response.status === 401 && !originalRequest._retry) {
//       originalRequest._retry = true;

//       try {
//         const accessToken = await refreshAccessToken();

//         // Update the Authorization header with the new access token
//         originalRequest.headers['Authorization'] = `Bearer ${accessToken}`;

//         // Retry the original request with the updated headers
//         return axios(originalRequest);
//       } catch {
//         throw 'refresh error.';
//       }
//     }

//     // For other errors, reject the request
//     return Promise.reject(error);
//   }
// );

export default api;
