// user.model.ts

export interface User {
  userId: string;
  role: Role;
  username: string;
  passwordHash: string;
}

export enum Role {
  User,
  Administrator,
}
