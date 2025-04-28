import { DatabaseDefault } from "./DatabaseDefault.js";

export class Keep extends DatabaseDefault {
constructor(d){
  super(d)
  this.name = d.name
  this.description = d.description
  this.img = d.img
  this.views = d.views
  this.creatorId = d.creatorId
  this.creator = d.creator
  this.kept = d.kept
}
}
export class VaultKeep extends Keep{
  constructor(d){
    super(d)
    this.vaultKeepId = d.vaultKeepId
  }
}