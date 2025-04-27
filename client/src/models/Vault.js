import { DatabaseDefault } from "./DatabaseDefault.js";

export class Vault extends DatabaseDefault{
  constructor(d){
    super(d)
    this.name = d.name
    this.description = d.description
    this.img = d.img
    this.isPrivate = d.isPrivate
    this.creatorId = d.creatorId
    this.creator = d.creator

  }
}