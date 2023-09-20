export class Article {
 headline?: string;
 articleId?: number;
 author?: string;
 body?: string;
 articleImgUrl?: string;

}

export class ResponseDto<T> {
  responseDate?: T;
  messageToClient?: string;
}
