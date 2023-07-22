export type LocalUser = {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  accessToken: string;
  expires: string;
};

export type AuthLoginCommand = {
  email: string;
  password: string;
};

export type LocalJwt = {
  accessToken: string;
  expires: string;
};

export type DecodedJwt = {
  uid: string;
  email: string;
  given_name: string;
  family_name: string;
  jti: string;
};
