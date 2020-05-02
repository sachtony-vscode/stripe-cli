//go:generate go run -tags=dev vfsgen.go

package stripe

import (
	"crypto/tls"
	"crypto/x509"
	"io/ioutil"
)

func GetTLSConfig() (*tls.Config, error) {
	caCertPool := x509.NewCertPool()

	f, err := FS.Open("/DigiCertGlobalRootCA.crt.pem")
	if err != nil {
		return nil, err
	}
	filedata, err := ioutil.ReadAll(f)
	if err != nil {
		return nil, err
	}
	caCertPool.AppendCertsFromPEM(filedata)

	f, err = FS.Open("/DigiCertHighAssuranceEVRootCA.crt.pem")
	if err != nil {
		return nil, err
	}
	filedata, err = ioutil.ReadAll(f)
	if err != nil {
		return nil, err
	}
	caCertPool.AppendCertsFromPEM(filedata)

	return &tls.Config{
		RootCAs: caCertPool,
	}, nil
}
