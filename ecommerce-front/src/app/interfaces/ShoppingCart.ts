import { OrderItem as OrderItem } from './OrderItem';
import { UserAggregate } from './UserAggregate';

export class ShoppingCart {
  ShoppingCartId?: number;
  UserAggregateId!: number;
  UserAggregate!: UserAggregate;
  OrderItems!: OrderItem[];
}
