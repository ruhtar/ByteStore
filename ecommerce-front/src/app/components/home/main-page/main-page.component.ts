import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {
  products: any[] = [
    {
      name: 'Produto 1',
      description: 'Descrição do Produto 1',
      imageUrl: 'URL_da_imagem_do_produto_1',
      price: 49.99
    },
    // Adicione mais produtos aqui
  ];

  constructor() { }

  ngOnInit(): void {
  }

  addToCart(product: any): void {
    // Lógica para adicionar o produto ao carrinho
  }
}
