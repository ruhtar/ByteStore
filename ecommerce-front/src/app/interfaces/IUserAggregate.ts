import { Roles } from '../enums/Roles';
import { IAddress } from './IAddress';
import { IUser } from './IUser';

export interface IUserAggregate {
  id: number;
  user: IUser;
  role: Roles;
  address: IAddress;
}
