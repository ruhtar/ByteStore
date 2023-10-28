import { Component } from '@angular/core';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.css'],
})
export class ReviewComponent {
  newCommentUser: string = '';
  newCommentText: string = '';

  // Lista de coment?rios existentes (voc? pode preench?-la com dados reais)
  comments: Comment[] = [
    { user: 'Comprador 1', text: 'COMENTARIO EXEMPLO1' },
    { user: 'Comprador 2', text: 'COMENTARIO EXEMPLO2' },
  ];

  // Fun??o para postar um novo coment?rio
  postComment() {
    // Verifique se o usu?rio preencheu ambos os campos
    if (this.newCommentUser && this.newCommentText) {
      // Adicione o novo coment?rio ? lista de coment?rios
      this.comments.push({
        user: this.newCommentUser,
        text: this.newCommentText,
      });

      // Limpe os campos do formul?rio ap?s a postagem
      this.newCommentUser = '';
      this.newCommentText = '';
    }
  }
}

// Interface para representar um coment?rio
interface Comment {
  user: string;
  text: string;
}
