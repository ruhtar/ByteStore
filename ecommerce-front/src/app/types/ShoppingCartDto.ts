import { Product } from './Product';

export class ShoppingCartDto {
  UserAggregateId?: number;
  ShoppingCartId!: number;
  products!: Product[];
}
