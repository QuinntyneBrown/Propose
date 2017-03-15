import { Link } from "./link.model";
import { EditorComponent } from "../shared";
import {  LinkDelete, LinkEdit, LinkAdd } from "./link.actions";

const template = require("./link-edit-embed.component.html");
const styles = require("./link-edit-embed.component.scss");

export class LinkEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    static get observedAttributes() {
        return [
            "link",
            "link-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.link ? "Edit Link": "Create Link";

        if (this.link) {                
            this._nameInputElement.value = this.link.name;  
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
        const link = {
            id: this.link != null ? this.link.id : null,
            name: this._nameInputElement.value
        } as Link;
        
        this.dispatchEvent(new LinkAdd(link));            
    }

    public onDelete() {        
        const link = {
            id: this.link != null ? this.link.id : null,
            name: this._nameInputElement.value
        } as Link;

        this.dispatchEvent(new LinkDelete(link));         
    }

    public link: Link;
    public linkId: any;

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "link-id":
                this.linkId = newValue;
                break;
            case "link":
                this.link = JSON.parse(newValue);
                if (this.parentNode) {
                    this.linkId = this.link.id;
                    this._nameInputElement.value = this.link.name != undefined ? this.link.name : "";
                    this._titleElement.textContent = this.linkId ? "Edit {{ entityNamePacalCase }}" : "Create {{ entityNamePacalCase }}";
                }
                break;
        }           
    }
    
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".link-name") as HTMLInputElement;}
}

customElements.define(`ce-link-edit-embed`,LinkEditEmbedComponent);
