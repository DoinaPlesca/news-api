export class Article {
  headline: string;
  articleId: number;
  articleImgUrl: string;
  body: string;
}

export class ResponseDto<T> {
  responseData: T;
  messageToClient?: string;
}