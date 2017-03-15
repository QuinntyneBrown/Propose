import { DigitalAsset } from "./digital-asset.model";
import { EditorComponent } from "../shared";
import {  DigitalAssetDelete, DigitalAssetEdit, DigitalAssetAdd } from "./digital-asset.actions";

const template = require("./digital-asset-edit-embed.component.html");
const styles = require("./digital-asset-edit-embed.component.scss");

export class DigitalAssetEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    static get observedAttributes() {
        return [
            "digital-asset",
            "digital-asset-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.digitalAsset ? "Edit Digital Asset": "Create Digital Asset";

        if (this.digitalAsset) {                
            this._nameInputElement.value = this.digitalAsset.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
    }

    public onSave() {
        const digitalAsset = {
            id: this.digitalAsset != null ? this.digitalAsset.id : null,
            name: this._nameInputElement.value
        } as DigitalAsset;
        
        this.dispatchEvent(new DigitalAssetAdd(digitalAsset));            
    }

    public onDelete() {        
        const digitalAsset = {
            id: this.digitalAsset != null ? this.digitalAsset.id : null,
            name: this._nameInputElement.value
        } as DigitalAsset;

        this.dispatchEvent(new DigitalAssetDelete(digitalAsset));         
    }

    public digitalAsset: DigitalAsset;
    public digitalAssetId: any;

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "digital-asset-id":
                this.digitalAssetId = newValue;
                break;
            case "digitalAsset":
                this.digitalAsset = JSON.parse(newValue);
                if (this.parentNode) {
                    this.digitalAssetId = this.digitalAsset.id;
                    this._nameInputElement.value = this.digitalAsset.name != undefined ? this.digitalAsset.name : "";
                    this._titleElement.textContent = this.digitalAssetId ? "Edit {{ entityNamePacalCase }}" : "Create {{ entityNamePacalCase }}";
                }
                break;
        }           
    }
    
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".digital-asset-name") as HTMLInputElement;}
}

customElements.define(`ce-digital-asset-edit-embed`,DigitalAssetEditEmbedComponent);
