import {Injectable} from "@angular/core";
import { Article } from "./models";

@Injectable({
  providedIn: 'root'
})
export class State {
  articles: Article[] = [];
}
