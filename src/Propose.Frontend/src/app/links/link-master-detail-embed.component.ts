import { linkActions } from "./link.actions";
import { Link } from "./link.model";

const template = require("./link-master-detail-embed.component.html");
const styles = require("./link-master-detail-embed.component.scss");

export class LinkMasterDetailEmbedComponent extends HTMLElement {
    constructor() {
        super();

        this.onLinkAdd = this.onLinkAdd.bind(this);
        this.onLinkDelete = this.onLinkDelete.bind(this);
        this.onLinkEdit = this.onLinkEdit.bind(this);
    }

    static get observedAttributes () {
        return [
            "links"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.linkListElement.setAttribute("links", JSON.stringify(this.links));
    }

    private _setEventListeners() {
        this.addEventListener(linkActions.ADD, this.onLinkAdd);
        this.addEventListener(linkActions.EDIT, this.onLinkEdit);
        this.addEventListener(linkActions.DELETE, this.onLinkDelete);
    }

    public onLinkAdd(e) {

        const index = this.links.findIndex(o => o.id == e.detail.link.id);
        const indexBasedOnName = this.links.findIndex(o => o.name == e.detail.link.name);

        if (index > -1 && e.detail.link.id != null) {
            this.links[index] = e.detail.link;
        } else if (indexBasedOnName > -1) {
            for (var i = 0; i < this.links.length; ++i) {
                if (this.links[i].name == e.detail.link.name)
                    this.links[i] = e.detail.link;
            }
        } else {
            this.links.push(e.detail.link);
        }
        
        this.linkListElement.setAttribute("links", JSON.stringify(this.links));
    }

    public onLinkEdit(e) {
        alert("edit");
    }

    public onLinkDelete(e) {
        alert("delete");
    }

    disconnectedCallback() {
        this.removeEventListener(linkActions.ADD, this.onLinkAdd);
        this.removeEventListener(linkActions.EDIT, this.onLinkEdit);
        this.removeEventListener(linkActions.DELETE, this.onLinkDelete);
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {

            case "links":
                this.links = JSON.parse(newValue);
                if (this.parentNode)
                    this.connectedCallback();

                break;
            default:
                break;
        }
    }

    public links: Array<Link> = [];
    public link: Link = <Link>{};
    public get linkEditElement(): HTMLElement { return this.querySelector("ce-link-edit-embed") as HTMLElement; }
    public get linkListElement(): HTMLElement { return this.querySelector("ce-link-list-embed") as HTMLElement; }

}

customElements.define(`ce-link-master-detail-embed`,LinkMasterDetailEmbedComponent);
