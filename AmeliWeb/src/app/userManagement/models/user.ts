import { BaseEntity } from '../../core/models/baseEntity';
import { primaryIdentifier } from '../../core/decorators/primaryIdentifier';


export class User extends BaseEntity {

  @primaryIdentifier
  public userID: number;

  public userName: string;

}
