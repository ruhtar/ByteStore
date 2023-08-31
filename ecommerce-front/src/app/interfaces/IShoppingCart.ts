import { IOrderItem } from './IOrderItem';
import { IUserAggregate } from './IUserAggregate';

export interface IShoppingCart {
  ShoppingCartId: number;
  UserAggregateId: number;
  UserAggregate: IUserAggregate;
  OrderItems: IOrderItem[];
}
