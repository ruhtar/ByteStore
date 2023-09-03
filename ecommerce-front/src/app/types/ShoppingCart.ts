import { OrderItem as OrderItem } from './OrderItem';

export class ShoppingCart {
  UserAggregateId?: number;
  ShoppingCartId!: number;
  OrderItems!: OrderItem[];
}
