export class BaseEntity {

  private readonly _idPoperties : Map<string, any> = new Map<string, any>();

  public get id(): any[] {
    return [...this.idProperties.values()];
  }

  public get idProperties() {
    return this._idPoperties;
  }
}



