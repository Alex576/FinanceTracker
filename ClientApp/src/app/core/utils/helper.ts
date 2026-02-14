export async function calculateHash(value: string): Promise<string> {
    var encoder = new TextEncoder().encode(value);
    var buffer = await crypto.subtle.digest('SHA-256', encoder);
    return Array.from(new Uint8Array(buffer)).map(x => x.toString(16).padStart(2, '0')).join('');
}