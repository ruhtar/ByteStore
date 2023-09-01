import { Roles } from '../enums/Roles';
import { Address } from './Address';
import { User } from './User';

export class UserAggregate {
  id?: number;
  user!: User;
  role!: Roles;
  address!: Address;
}
