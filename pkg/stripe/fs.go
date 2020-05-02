//+build dev

package stripe

import "net/http"

// FS exports the filesystem
var FS http.FileSystem = http.Dir("../../data")
